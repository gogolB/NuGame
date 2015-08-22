#! /bin/sh

# This file is responsible for checking the output log from the build.

PATTERN="Compilation failed"

if grep -q $PATTERN $(pwd)/unity.log; then
    exit 1
else
    exit 0
fi
