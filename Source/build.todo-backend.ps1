$reg = "ursu"
$app = "ant-todo-api"
$tag = Get-Date -Format "HHmm"

$image = "${reg}/${app}:${arch}-${tag}"

docker build -t $image -f "./Dockerfile.todo-api" .
docker push $image

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');