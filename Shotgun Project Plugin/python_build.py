from System import IO

import sys
import clr

def walk(dir):
    for file in IO.Directory.GetFiles(dir):
        yield file
    for dir in IO.Directory.GetDirectories(dir):
        for file in walk(dir):
            yield file

if __name__ == "__main__":
    all_files = list(walk(sys.argv[1]))
    exceptions = [
        "shotgun_api3\lib\httplib2\cacerts.txt",
        "shotgun_api3\lib\simplejson\_speedups.c",
        "site-packages\README.txt",
    ]
    files = [e for e in all_files if e[len(sys.argv[1])+1:] not in exceptions]
    print "Compiling:\n   %s" % "\n   ".join([e[len(sys.argv[1])+1:] for e in files])
    clr.CompileModules(sys.argv[2], *files)
    print "Created %s" % sys.argv[2]