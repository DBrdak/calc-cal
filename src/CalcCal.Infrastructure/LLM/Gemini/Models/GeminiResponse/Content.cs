﻿namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

internal sealed class Content
{
    public Part[] Parts { get; set; }
    public string Role { get; set; }
}