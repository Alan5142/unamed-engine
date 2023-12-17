//
//  LibManager.hpp
//  embedding_test
//
//  Created by Leonidas Neftali Gonzalez Campos on 13/12/23.
//
#pragma once
#if WIN32
#include <windows.h>
#else
#include <dlfcn.h>
#endif
/// <summary>
/// Provides easy access to cross platform library loading functions
/// </summary>
class LibManager {
public:
	static void* LibraryOpen(const char* libraryPath);

	static void* DynamicLoadSymbol(void* handle, const char* symbol);


};
