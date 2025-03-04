﻿using FluentAssertions;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using System;
using System.Globalization;
using Xunit;

namespace SpendSmart.Domain.UnitTests.Modules.Common;

public class CurrencyTests
{
    private static readonly IFormatProvider NumberFormat = new CultureInfo("sr-RS");

    [Fact]
    public void Format_ShouldProperlyFormatAmount()
    {
        // Arrange
        decimal amount = 1.097m;
        Currency currency = CurrencyTestData.DefaultCurrency;

        // Act
        string formatted = currency.Format(amount);

        // Assert
        formatted.Should().Be($"{amount.ToString("N2", NumberFormat)} {currency.Code}");
    }
}
