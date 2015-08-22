#! /bin/sh

# This file is responsible for checking the output log from the build.

PATTERN="Compilation failed"

ls -al
ls -al $(pwd)

if grep -q $PATTERN "$(pwd)/unity.log"; then
    exit 0
else
    exit 1
fi
