# Perfect-Squares
An interesting problem in arithmetic with deep implications to elliptic curve theory is the problem of finding perfect squares that are sums of consecutive squares. A classic example is the Pythagorean identity:

3^2 + 4^2 = 5^2 (1) 

that reveals that the sum of squares of 3, 4 is itself a square. 
A more interesting example is Lucas‘ Square Pyramid:

1^2 +2^2 +...+24^2 =70^2 (2)

In both of these examples, sums of squares of consecutive integers form the square of another integer.
The goal of this project is to use F# and the actor model to build a good solution to this problem that runs well on multi-core machines.

Input: The input provided will be two numbers: N and k. The overall goal is to find all k consecutive numbers starting at 1 and up to N, such that the sum of squares is itself a perfect square (square of an integer).

To start, run dotnet fsi psquare.fsx N k

e.g. 

Example 1: 

dotnet fsi psquare.fsx 3 2

[3]

Example 2:

dotnet fsi psquare.fsx 40 24

[1, 9, 20, 25]

Example 3:

dotnet fsi psquare.fsx 100000 4

[]
