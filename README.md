# WPF-SwitchResourceDictionary
WPF-Switch resource dictionary and language example, This example mainly shows how to switch the resource dictionary to achieve switching application display and Themes.


Pack Uri solution
--------
As we all know, the URI path identification scheme we generally use starts with http, Ftp, and file (prefix). The Pack Uri scheme we are talking about here does not use these, and uses pack as its prefix, so our path is similar to this pack ://Organization/path.

Organization, which identifies the package type of the part.
Path, which identifies the location of the component in the package.

One or more files are stored in a package, for example

1.Resource files compiled in local assembly
2.Compile to the resource file in the referenced assembly
3.Compile to the resource file in the reference assembly
4.Content file
5.Source site file

WPF supports two ways
--------
application:/// is used to identify application data files known at compile time, including resource files and content files.
siteiforigin:/// is used to identify the source site file

For specific URI format, please refer to standard [RFC2396](https://blog.csdn.net/hotmee/article/details/107298462) 

Resource file Pack Uri
local assembly resources
--------
The so-called resource file refers to the configuration of the file as Resource, which is compiled as an assembly after MSBuild. The resource file can be in the current assembly or reference the assembly. 

![image](https://user-images.githubusercontent.com/20103128/141066212-faa28ab7-72d3-47c5-8ae3-88122e388451.png)

The resource file access format compiled to the current assembly is as follows: Authorization;,,,/path

Authorization: application:///
Path The name of the resource file, including its path relative to the root directory of the local assembly folder. 

The following demo Pack Uri path is the resource file in the current folder:

```csharp
Uri uri = new Uri("Themes/Theme.xaml", UriKind.RelativeOrAbsolute);
Uri uri = new Uri("/WpfApp;component/Themes/en-Us.gif", UriKind.RelativeOrAbsolute);
Uri uri = new Uri("pack://application:,,,/WpfApp;component/Themes/en-Us.gif", UriKind.RelativeOrAbsolute);
```

Reference assembly resources 
--------
The reference assembly resource path is a bit more complicated than the local assembly, the format is as follows
`Assembly short name {;version}{;public key};component/path`
1. Assembly short name: the short name of the referenced assembly
2. Version [optional]: Assembly version information, used when loading multiple reference assemblies with the same name.
3. Public key [optional]: The public key of the signature relative to the application assembly. Used when loading multiple reference assemblies with the same name.
4. Component: Specify the name of the assembly referenced by the current assembly
/Path: The name of the resource file. If it exists in a subfolder, then the subfolder must also be included here.

The following demo Pack Uri path is the resource file in the current folder:

```csharp
Uri uri = new Uri("/WpfLibrary;v1.0.0.1;component/Themes/Theme.xaml", UriKind.RelativeOrAbsolute);
Uri uri = new Uri("/WpfLibrary;component/Themes/en-Us.gif", UriKind.RelativeOrAbsolute);
Uri uri = new Uri("pack://application:,,,/WpfLibrary;component/Themes/en-Us.gif", UriKind.RelativeOrAbsolute);
```

Source site Pack Uri
--------
The pack Uri path format authorization of the source site file,,,/path

Authorization: siteoforgin:///
Path: The path to the start location of the executable assembly of the source site file 

The following example refers to the folder where the startup assembly is located:

```csharp
Uri uri = new Uri("pack://siteoforgin:,,,/Themes/Theme.xaml");
Uri uri = new Uri("pack://siteoforgin:,,,/WpfLibrary;component/Themes/en-Us.gif");
```

Use PackUri in markup
Absolute URI 
--------
|                                                  File |                        Absolute package URI |
|------------------------------------------------------ |-------------------------------------------- |
|                         Resource file-local assembly  |            pack://application:,,,/Theme.xaml |
|               Resource file-local assembly-subfolder  |            pack://application:,,,/Themes/Theme.xaml |
|                     Resource file-reference assembly  |            pack://application:,,,/WpfLibrary;component/Theme.xaml |
|           Resource file-reference assembly-subfolder  |            pack://application:,,,/WpfLibrary;component/Themes/Theme.xaml |
|   Resource file-reference assembly-version-subfolder  |            pack://application:,,,/WpfLibrary;v1,0,0,2;component/Themes/Theme.xaml |
|                                     Source site file  |            pack://siteoforigin:,,,/Theme.xaml |
|                         Source site files-subfolders  |            pack://siteoforigin:,,,/Themes/Theme.xaml |


Relative URI
--------
|                                                  File |                        Relative package URI |
|------------------------------------------------------ |-------------------------------------------- |
|                    Resource access in local assembly  |            /Theme.xaml |
|          Resource access in local assembly-subfolder  |            /Themes/Theme.xaml |
|         Reference to resource access in the assembly  |            /WpfLibrary;component/Theme.xaml |
|   Reference to resource access in assembly-subfolder  |            /WpfLibrary;component/Themes/Theme.xaml |
