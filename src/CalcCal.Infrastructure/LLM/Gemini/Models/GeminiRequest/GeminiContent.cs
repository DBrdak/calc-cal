﻿namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiRequest;

internal sealed class GeminiContent
{
    public string Role { get; set; }
    public GeminiPart[] Parts { get; set; }
}