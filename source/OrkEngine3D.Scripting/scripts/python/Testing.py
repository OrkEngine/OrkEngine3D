import clr
import System
clr.AddReference("System.Core")
clr.ImportExtensions(System.Linq)
from System import String

print('hello, world')

def adder(arg1, arg2):
    return arg1 + arg2

class MyClass(object):
    def __init__(self, value):
        self.value = value

print(String.Format("This {0}", "Works"))