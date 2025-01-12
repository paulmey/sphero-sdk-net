﻿using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using shpero.Rvr.Protocol;

namespace shpero.Rvr
{
    public static class DriverExtensions
    {
        public static async Task<Message> SendRequestAsync(this Driver driver, Message request, CancellationToken cancellationToken = default)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var cs = new TaskCompletionSource<Message>(TaskCreationOptions.RunContinuationsAsynchronously);

            var sub = driver.Where(m =>
                (m.Header.Sequence == request.Header.Sequence) &&
                (m.Header.DeviceId == request.Header.DeviceId) &&
                (m.Header.CommandId == request.Header.CommandId))
                .Timeout(TimeSpan.FromSeconds(10)).Subscribe(response =>
            {
                cs.TrySetResult(response);
            }, exception =>
            {
                cs.TrySetException(exception);
            });

            var _ = driver.SendAsync(request, cancellationToken);

            return await cs.Task.ContinueWith(t =>
            {
                sub.Dispose();
                return t.Result;
            }, cancellationToken);
        }
    }
}