using System;
using Microsoft.Extensions.DependencyInjection;
using VoyadoTest.Services;

namespace VoyadoTest.Infrastructure;

public interface IBingSearchFactory
{
    IBingSearch GetBingSearch();
}

public class BingSearchFactory : IBingSearchFactory
{
    private readonly IServiceProvider _serviceProvider;

    public BingSearchFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IBingSearch GetBingSearch()
    {
        return _serviceProvider.GetRequiredService<IBingSearch>();
    }
}