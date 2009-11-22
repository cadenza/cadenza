MCS = gmcs
MCS_FLAGS = --langversion:linq

Mono.Rocks.dll: Mono.Rocks.dll.sources
	$(MCS) $(MCS_FLAGS) -t:library -out:Mono.Rocks.dll @Mono.Rocks.dll.sources

clean:
	rm -f Mono.Rocks.dll

run-test:
	cd Tests; $(MAKE) run-test; cd ..;
