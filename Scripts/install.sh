#! /bin/sh

# Example install script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# This path will need to be set everytime unity updates their builder. We could also just get a copy and host it on one of our own servers, Probably will have a slower download time.
echo 'Downloading from http://netstorage.unity3d.com/unity/5b98b70ebeb9/MacEditorInstaller/Unity.pkg'
curl -O http://netstorage.unity3d.com/unity/afd2369b692a/MacEditorInstaller/Unity-5.1.2f1.pkg

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity-5.1.2f1.pkg -target / &
while true; do
  ps -p$! 2>& 1>/dev/null
  if [ $? = 0 ]; then
    echo "still going"; sleep 10
  else
    break
  fi
done
