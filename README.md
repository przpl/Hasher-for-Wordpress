# Hasher-for-Wordpress
Tiny library written in C# for calculating password hashes for Wordpress. Compatible platforms: .NET Core & .NET Framework.

## Usage

Create instance:
```cs
using WordpressPasswordHasher;

IWpPasswordHasher hasher = new WpPasswordHasher();
```

Hash:
```cs
string hash = hasher.Hash("test", "$P$B55D6LjfH"); // result is "$P$B55D6LjfHDkINU5wF.v2BuuzO0/XPk/"
```

Verify:
```cs
bool result = hasher.Verify("$P$B55D6LjfHDkINU5wF.v2BuuzO0/XPk/", "test"); // result is true
```
