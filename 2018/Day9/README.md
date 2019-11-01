# Part 1

Part 1 is efficient enough (a few seconds), since it's just `int` representing the Marble. It's approximately `O(n^2)`.

# Part 2

Part 2 is another ball game; making the last marble 100 times larger means the problem size becomes `100^2` _more_ difficult. Apparently a deque is the correct data structure to solve this in a reasonable time.
