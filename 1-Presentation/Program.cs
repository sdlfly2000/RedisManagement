using Core.ServiceRegistration;
using Microsoft.Extensions.DependencyInjection;
using RedisCtl;

using var serviceProvider = ServiceRegister
                                .Register("RedisCtl", "Application.Services")
                                .BuildServiceProvider();

var worker = serviceProvider.GetRequiredService<Worker>();

await worker.Execute(args);