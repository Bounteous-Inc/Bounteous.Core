﻿namespace Bounteous.Core.Utilities.Mapper.Converter;

public class StringConverter : AbstractValueConverter<string>
{
    protected override string InternalConvert(string input)
        => input;
}