# ShapeFX

ShapeFX unifies under a simple API the 3 most common Dynamic Compilation engines for C# - Roslyn, CodeDom and Mono CSharp Compiler.

If you want to start playing around with Dynamic Compilation and don't know where to start or don't have time to be caught in all the compiler specific settings and details, ShapeFX is your new best friend. Just handle it a C# class as a string, choose the compilation engine you want and see your type generated at runtime ready to be used inside your app.

ShapeFX is built with extensibility in mind so you can even plug in other compilation engines besides the ones mentioned above. 

# Instructions
<h3>Compiling a new type:</h3>
1 - Get your class source code into a string.
```
var code = File.ReadAllText("PathToYourClassFile");
```
2 - Pass the assembly dependencies you need for your type to compile (if you don't pass anything the below line will be automatically called).
```
var dependencies = AppDomain.CurrentDomain.GetAssemblies().ToArray()
```

3 - Instantiate your compiler based on the compilation engine of your choice. You can select one from the 3 built-in compiler class engines, RoslynEngine, MonoEngine and CodeDomEngine. You can even plug in our own by implementing the IEngine interface.
```
var compiler = new Compiler<RoslynEngine>();
```

4 - Compile your type, by calling the Compile method of the compiler you created.
```
// The false flag if for generating debug information on your dynamic assembly)
var type = compiler.Compile(code, dependencies, true); 
```

5 - Create an instance of your dynamically generated type.
```
// Pass any constructor dependencies that you might have on your type, instead of null
var instance = Activator.CreateInstance(type, null);
```

6 - Call any method on your new type.
```
instance.MyMethod()
```
