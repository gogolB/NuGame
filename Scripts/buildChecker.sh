#! /bin/sh

# This file is responsible for checking the output log from the build.

if grep -q "Compilation failed" unity.log; then
    exit 1
else
    exit 0
fi
