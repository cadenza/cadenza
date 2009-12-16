MCS = gmcs
MONODOCER = monodocer
srcdir=.
PACKAGE = cadenza
VERSION = 0.1.0

prefix = /usr/local
libdir = $(prefix)/lib

mrdir  = lib/cadenza
pkdir  = lib/pkgconfig

# warning CS1591: Missing XML comment for publicly visible type or member...
MCS_FLAGS = -nowarn:1591

# in tests, CS0219: The variable ... was assigned/declared but not used.
TST_FLAGS = -nowarn:0219,0168

.PHONY: all check-gendarme check clean install shell

$(mrdir)/Cadenza.dll: Cadenza.dll.sources $(shell cat Cadenza.dll.sources)
	$(MCS) -doc:$(mrdir)/Cadenza-in.xml -debug+ -t:library -r:System.Core -out:$@ $(MCS_FLAGS) @Cadenza.dll.sources

all: $(mrdir)/Cadenza.dll

include src/Cadenza/Documentation/Makefile.include

install: doc-install
	-mkdir -p $(libdir)/cadenza $(libdir)/pkgconfig
	cp $(mrdir)/Cadenza.dll* $(libdir)/cadenza
	cp $(pkdir)/*.pc   $(libdir)/pkgconfig

check-gendarme:
	gendarme --html errors.html --ignore gendarme.ignore $(mrdir)/Cadenza.dll

clean: doc-clean
	rm -f $(mrdir)/*.dll*

$(mrdir)/Cadenza_test.dll: Cadenza_test.dll.sources $(shell cat Cadenza_test.dll.sources) $(mrdir)/Cadenza.dll
	$(MCS) -debug+ -r:$(mrdir)/Cadenza.dll -r:System.Core -pkg:mono-nunit -t:library -out:$@ $(MCS_FLAGS) $(TST_FLAGS) @Cadenza_test.dll.sources

check: $(mrdir)/Cadenza_test.dll
	nunit-console2 -exclude:NotWorking $(NUNIT_OPTIONS) $(mrdir)/Cadenza_test.dll

check-all: $(mrdir)/Cadenza_test.dll
	nunit-console2  $(NUNIT_OPTIONS) $(mrdir)/Cadenza_test.dll

shell:
	csharp -r:$(mrdir)/Cadenza.dll

