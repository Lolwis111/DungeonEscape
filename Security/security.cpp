#include "security.h"

namespace security
{
	int checkLevel(int emptyCount, int blockCount, int spriteCount)
	{
		int s = emptyCount * 7;
		int t = blockCount * 5;
		int u = spriteCount * 3;

		return s + u + t;
	}

	int checkItems(int pliers, int keys, int pickaxes, int emptys)
	{
		int p = (pliers + 10) * 3;
		int k = (keys + 9) * 4;
		int p2 = (pickaxes + 8) * 5;
		int e = (emptys + 7) * 6;

		return p + k + p2 + e;
	}
};
