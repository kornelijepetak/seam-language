﻿namespace SeamCompiler.LexicalAnalysis;

public readonly record struct Position(int Line, int Column, int Offset);
