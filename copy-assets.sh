#!/bin/bash
echo "Copying Dulagun assets..."
# This assumes you have the assets repo cloned to the same root folder as the game itself
mkdir -p ./src/assets
cp -r ../dulagun-assets/assets/* ./src/assets
echo "Complete!"
