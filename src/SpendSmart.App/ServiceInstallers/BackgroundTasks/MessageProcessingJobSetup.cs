using Microsoft.Extensions.Options;
using Quartz;
using SpendSmart.BackgroundTasks.MessageProcessing;
using SpendSmart.BackgroundTasks.MessageProcessing.Options;

namespace SpendSmart.App.ServiceInstallers.BackgroundTasks;

/// <summary>
/// Represents the <see cref="MessageProcessingJob"/> setup.
/// </summary>
public sealed class MessageProcessingJobSetup : IPostConfigureOptions<QuartzOptions>
{
    private readonly MessageProcessingJobOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageProcessingJobSetup"/> class.
    /// </summary>
    /// <param name="options">The message processing job options.</param>
    public MessageProcessingJobSetup(IOptions<MessageProcessingJobOptions> options) =>
        _options = options.Value;

    /// <inheritdoc />
    public void PostConfigure(string name, QuartzOptions options)
    {
        var jobKey = new JobKey(nameof(MessageProcessingJob));

        options.AddJob<MessageProcessingJob>(jobBuilder => jobBuilder.WithIdentity(jobKey));

        options.AddTrigger(triggerBuilder => triggerBuilder
            .ForJob(jobKey)
            .WithSimpleSchedule(scheduleBuilder =>
                scheduleBuilder.WithIntervalInSeconds(_options.IntervalInSeconds).RepeatForever()));
    }
}
