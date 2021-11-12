## How to run k8s
1. You need minikube installed locally.
2. Enable **Ingress** addon:\
`minikube addons enable ingress`
5. Enable automatic images load to minikube. **Note: you need to run these commands in PowerShell**\
<!-- `minikube docker-env`\ -->
`minikube docker-env | Invoke-Expression`
4. Build docker images. **Note: you need to run these commands in the same terminal**\
`docker-compose -f docker-compose.k8s-build.yml build`
8. Create namespace:\
`kubectl create -f media-lake.namespace.yml`
8. Install istio and run:
`kubectl label namespace media-lake istio-injection=enabled`
<!-- 9. Create .env folder using .env.example. Load these secrets to k8s:\
`kubectl create secret generic learn-api-secrets --from-env-file=.env/learn.env --namespace=campus-namespace`
`kubectl create secret generic users-api-secrets --from-env-file=.env/users.env --namespace=campus-namespace` -->
10. Run k8s:\
`kubectl apply -f gateway-api -f users-api -f core-api -f gateway.yml -n media-lake`
11. Run `sudo minikube tunnel` and `kubectl get svc istio-ingressgateway -n istio-system` to get running service ip

## Useful commands:
1. Open minikube dashboard:\
`minikube dashboard`
<!-- 2. Delete secrets:\
`kubectl delete secret learn-api-secrets --namespace=campus-namespace`
`kubectl delete secret users-api-secrets --namespace=campus-namespace` -->
3. Restart deployment:\
`kubectl rollout restart deployment/core-api-deployment -n=media-lake`\
`kubectl rollout restart deployment/users-api-deployment --namespace=campus-namespace`\
`kubectl rollout restart deployment/frontend-deployment --namespace=campus-namespace`
`kubectl rollout restart deploy -n=media-lake`
4. Get deployment logs:\
`kubectl logs deploy/core-api-deployment -n=media-lake`
`kubectl logs deploy/frontend-deployment --namespace=campus-namespace`
6.
`kubectl delete --all deployments --namespace=campus-namespace`
6. Delete everything:\
`kubectl delete all --all -n media-lake`
