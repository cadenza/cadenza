# Fixup //member/@name (because gmcs output doesn't match CSC convention)

# Type.ICollection.Method``(args)
/<member name=.*\.\(IList\|ICollection\|IEnumerable\)\..*``1/ {
	s/\(<member name=".*\).\(IList\|ICollection\|IEnumerable\)\.\(.*\)``1/\1.System#Collections#Generic#\2{System#Object}#\3/
	p
	d
}

# Type.ICollection`1.Property
/<member name=.*\.\(IList\|ICollection\)`1\./ {
	s/\(<member name=".*\)\.\(IList\|ICollection\)`1\.\(.*\)/\1.System#Collections#Generic#\2{System#Object}#\3/
	p
	d
}

# Type.ICollection.Member
/<member name=.*\.\(IList\|ICollection\|IEnumerable\)\./ {
	s/\(<member name=".*\).\(IList\|ICollection\|IEnumerable\).\(.*\)/\1.System#Collections#\2#\3/
	p
	d
}

# workaround https://bugzilla.novell.com/show_bug.cgi?id=425898
/<member name=.*Tuple`.\.Match``1(System.Func`.*)"/ {
	s/System.Func`.\[/System.Func{/
	s/T1/`0/g
	s/T2/`1/g
	s/T3/`2/g
	s/T4/`3/g
	# `T' is used in Tuple<T>
	s/\[Cadenza.Maybe.*=null\]\]/Cadenza.Maybe{``0}}/
	s/System.Func{T,/System.Func{`0,/g
	p
	d
}

