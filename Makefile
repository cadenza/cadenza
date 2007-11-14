MCS = gmcs
MCS_FLAGS = -langversion:linq

Mono.Rocks.dll: Mono.Rocks.dll.sources
	$(MCS) -t:library -r:System.Core -out:Mono.Rocks.dll $(MCS_FLAGS) @Mono.Rocks.dll.sources

all: Mono.Rocks.dll

clean:
	rm -f *.dll

run-test:

Mono.Rocks.Tests.dll: Mono.Rocks.Tests.dll.sources
	$(MCS) -r:Mono.Rocks.dll -r:System.Core -pkg:mono-nunit -t:library -out:Mono.Rocks.Tests.dll $(MCS_FLAGS) @Mono.Rocks.Tests.dll.sources

run-test: Mono.Rocks.Tests.dll
	nunit-console2 Mono.Rocks.Tests.dll
