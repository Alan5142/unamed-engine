#include "ScriptingManager.hpp"
#include <iostream>

#if WIN32
constexpr std::string_view DOTNET_PATH = "C:/Program Files/dotnet/";
#elif __APPLE__
constexpr std::string_view DOTNET_PATH = "/usr/local/share/dotnet";
#else
constexpr std::string_view DOTNET_PATH = "/usr/share/dotnet";
#endif

struct DemoStruct
{
	int32_t a;
	int32_t b;
};

int main()
{
	//Demo stuff
	auto scriptManager = ScriptingManager(DOTNET_PATH.data());
	const char* assembly = "assembly-test";
	const char* testNamespace = "Test";
	const char* testClass = "Class1";
	const char* testFunc = "SumTest";

	for (float i = 0.0f; i < 10000.0f; i++)
	{
		float result = scriptManager.InvokeCSharpWithReturn<float>(assembly, testNamespace, testClass, "SqrRootTest", i, 0.5f);
		printf("From sqr root in C#: %f\n", result);
	}

	return 0;
}
