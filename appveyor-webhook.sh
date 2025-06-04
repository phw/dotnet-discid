#!/bin/sh
set -e

. ~/.appveyor

cd $(dirname $0)

# Get commit details
if [ -z "$GIT_REF" ]; then
  ref=$(git symbolic-ref -q HEAD)
else
  ref="$GIT_REF"
fi
commitId=$(git rev-parse HEAD)
message=$(git log --format='%B' $commitId^\!)
author=$(git log --format='%an' $commitId^\!)
email=$(git log --format='%ae' $commitId^\!)
timestamp=$(git log --format='%ci' $commitId^\!)

# Construct payload JSON
author=$(jq -n \
  --arg name "$author" \
  --arg email "$email" \
  '{name: $name, email: $email}'
)
commit=$(jq -n \
  --arg id "$commitId" \
  --arg message "$message" \
  --arg timestamp "$timestamp" \
  --argjson author "$author" \
  '{id: $id, message: $message, timestamp: $timestamp, author: $author}'
)
payload=$(jq -n \
  --arg ref "$ref" \
  --argjson commit "$commit" \
  '{ref: $ref, head_commit: $commit}'
)

echo $payload

# Trigger webhook
curl --silent \
  --location "$APPVEYOR_WEBHOOK" \
  --header "Content-Type: application/json" \
  --data "$payload"
