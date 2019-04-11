#define _CRT_SECURE_NO_WARNINGS // yes MSVC, I want to use fopen
#include "etc.h"

#define SOKOL_IMPL
#include <vector>
#include <stdio.h>
#include <string>
#include <chrono>
#include <iostream>

typedef std::vector<uint8_t> ByteArray;

static void ReadFile(const char* fileName, ByteArray& output)
{
	FILE* f = fopen(fileName, "rb");
	if (f)
	{
		fseek(f, 0, SEEK_END);
		size_t size = ftell(f);
		fseek(f, 0, SEEK_SET);
		size_t pos = output.size();
		output.resize(pos + size);
		fread(output.data() + pos, size, 1, f);
		fclose(f);
	}
}

static void WriteFile(const char* fileName, ByteArray& output)
{
	FILE* f = fopen(fileName, "wb");
	if (f)
	{
		fwrite(output.data(), output.size(), 1, f);
		fclose(f);
	}
}

int main(int argc, char *argv[])
{
	if (argc < 5)
	{
		std::cout << "Format: {fileName} {width} {height} {mode}" << std::endl;
		std::cout << "mode: 0 - ETC; 1 - ETC2; 2 - ETC2a1; 3 - ETC2a8" << std::endl;
		std::cin.get();
		return 0;
	}

	ByteArray input;
	char appDirectory[256];
	_splitpath(argv[0], nullptr, appDirectory, nullptr, nullptr);
	char* filePath = argv[1];
	char fileDirectory[256];
	char fileName[256];
	_splitpath(filePath, nullptr, fileDirectory, fileName, nullptr);
	int width = std::stoi(argv[2]);
	int height = std::stoi(argv[3]);
	int mode = std::stoi(argv[4]);
	std::string appDirStr = std::string(appDirectory);
	std::string inPath = fileDirectory[0] == 0 ? appDirStr + filePath : filePath;
	ReadFile(inPath.c_str(), input);

	ByteArray output;
	output.resize(width * height * 4);
	for (int j = 0; j < 5; j++)
	{
		auto start = std::chrono::high_resolution_clock::now();
		for (int i = 0; i < 10; i++)
		{
			switch (mode)
			{
			case 0:
				decode_etc1(input.data(), width, height, (uint32_t*)output.data());
				break;
			case 1:
				decode_etc2(input.data(), width, height, (uint32_t*)output.data());
				break;
			case 2:
				decode_etc2a1(input.data(), width, height, (uint32_t*)output.data());
				break;
			case 3:
				decode_etc2a8(input.data(), width, height, (uint32_t*)output.data());
				break;

			default:
				throw (std::string("unknown mode ") + argv[4]).c_str();
			}
		}
		auto finish = std::chrono::high_resolution_clock::now();
		auto microseconds = std::chrono::duration_cast<std::chrono::milliseconds>(finish - start);
		std::cout << microseconds.count() << "ms\n";
	}

	std::string outPath = appDirStr + fileName + ".data";
	//WriteFile(outPath.c_str(), output);
}
