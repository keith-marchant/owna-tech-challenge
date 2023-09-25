using System.Diagnostics;
using OWNA.ECommerce.Application.Exceptions;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;

namespace OWNA.ECommerce.Api.ProblemDetails;

public static class ProblemDetailsConfiguration
    {
        public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();

                options.OnBeforeWriteDetails = (ctx, problem) =>
                {
                    // Inject the traceId into all problem detail responses.
                    problem.Instance = Activity.Current?.Id ?? ctx.TraceIdentifier;
                };

                // Configure problem details per exception type here.
                options.Map<NotFoundException>(ex => new NotFoundProblemDetails(ex));
                options.Map<ValidationException>(ex => new BadRequestProblemDetails(ex));
                options.Map<ArgumentOutOfRangeException>(ex => new BadRequestProblemDetails(ex));
                options.Map<InvalidArgumentException>(ex => new BadRequestProblemDetails(ex));
                options.Map<AggregateException>(ex =>
                {
                    return ex.InnerException switch
                    {
                        null => new UnhandledExceptionProblemDetails(ex),
                        ValidationException validation => new BadRequestProblemDetails(validation),
                        NotFoundException notFound => new NotFoundProblemDetails(notFound),
                        ArgumentOutOfRangeException argOutOfRange => new BadRequestProblemDetails(argOutOfRange),
                        InvalidArgumentException invalidArg => new BadRequestProblemDetails(invalidArg),
                        _ => new UnhandledExceptionProblemDetails(ex.InnerException),
                    };
                });

                // This must always be last as this will serve to handle unhandled exceptions.
                options.Map<Exception>(ex => new UnhandledExceptionProblemDetails(ex));
            });
            
            return services;
        }
    }