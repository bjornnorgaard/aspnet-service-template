$arch = "x86"
$reg = "ursu"
$app = "ant-todo-backend"
$tag = Get-Date -Format "HHmm"

$image = "${reg}/${app}:${arch}-${tag}"

docker build -t $image -f "./Dockerfile.todo-backend.${arch}" .
docker push $image

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');