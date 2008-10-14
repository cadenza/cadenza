MCS = gmcs
MONODOCER = monodocer
srcdir=.
PACKAGE = mono-rocks
VERSION = 0.1.0

prefix = /usr/local
libdir = $(prefix)/lib

mrdir  = lib/mono-rocks
pkdir  = lib/pkgconfig

# warning CS1591: Missing XML comment for publicly visible type or member...
MCS_FLAGS = -nowarn:1591

# in tests, CS0219: The variable ... was assigned/declared but not used.
TST_FLAGS = -nowarn:0219,0168

.PHONY: all check-gendarme check clean install shell

$(mrdir)/Mono.Rocks.dll: Mono.Rocks.dll.sources $(shell cat Mono.Rocks.dll.sources)
	$(MCS) -doc:doc/mono-rocks.xml -debug+ -t:library -r:System.Core -out:$@ $(MCS_FLAGS) @Mono.Rocks.dll.sources

all: $(mrdir)/Mono.Rocks.dll

include doc/Makefile.include

install: doc-install
	-mkdir -p $(libdir)/mono-rocks $(libdir)/pkgconfig
	cp $(mrdir)/Mono.Rocks.dll* $(libdir)/mono-rocks
	cp $(pkdir)/*.pc   $(libdir)/pkgconfig

mkdelegates mkeithers mklambda mktuples : Generator.pm

Mono.Rocks/Eithers.cs : mkeithers Makefile
	./mkeithers -n 4 > $@

Mono.Rocks/Tuples.cs : mktuples Makefile
	./mktuples -n 4 > $@

Mono.Rocks/Lambdas.cs : mklambda Makefile
	./mklambda -n 4 > $@

Mono.Rocks/Delegates.cs : mkdelegates Makefile
	./mkdelegates -n 4 > $@

check-gendarme:
	gendarme --html errors.html --ignore gendarme.ignore $(mrdir)/Mono.Rocks.dll

clean: doc-clean
	rm -f $(mrdir)/*.dll*

$(mrdir)/Mono.Rocks.Tests.dll: Mono.Rocks.Tests.dll.sources $(shell cat Mono.Rocks.Tests.dll.sources) $(mrdir)/Mono.Rocks.dll
	$(MCS) -debug+ -r:$(mrdir)/Mono.Rocks.dll -r:System.Core -pkg:mono-nunit -t:library -out:$@ $(MCS_FLAGS) $(TST_FLAGS) @Mono.Rocks.Tests.dll.sources

check: $(mrdir)/Mono.Rocks.Tests.dll
	nunit-console2 /exclude:NotWorking $(mrdir)/Mono.Rocks.Tests.dll

check-all: $(mrdir)/Mono.Rocks.Tests.dll
	nunit-console2 $(mrdir)/Mono.Rocks.Tests.dll

shell:
	csharp -r:$(mrdir)/Mono.Rocks.dll

