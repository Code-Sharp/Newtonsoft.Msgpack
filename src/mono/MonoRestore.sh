# Linux certificate handling
sudo mozroots --import --machine --sync
sudo certmgr -ssl -m https://go.microsoft.com
sudo certmgr -ssl -m https://nugetgallery.blob.core.windows.net
sudo certmgr -ssl -m https://nuget.org

#!/bin/sh -x
mkdir -p packages
cd packages
for i in .nuget Newtonsoft.Msgpack
  do mono --runtime=v4.0 ../.nuget/NuGet.exe install ../$i/packages.config
done
cd ..
