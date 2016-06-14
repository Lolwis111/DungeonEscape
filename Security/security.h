#pragma once

namespace security
{
	extern "C" __declspec(dllexport) int __cdecl checkLevel(int emptyCount, int blockCount, int spriteCount);
	__declspec(dllexport) int __cdecl checkLevel(int emptyCount, int blockCount, int spriteCount);

	extern "C" __declspec(dllexport) int __cdecl checkItems(int pliers, int keys, int pickaxes, int emptys);
	__declspec(dllexport) int __cdecl checkItems(int pliers, int keys, int pickaxes, int emptys);
}