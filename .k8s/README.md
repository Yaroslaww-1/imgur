## How to run k8s
1. You need minikube installed locally.
2. Enable **Ingress** addon:

`minikube addons enable ingress`

5. Enable automatic images load to minikube. **Note: you need to run these commands in PowerShell**

`minikube docker-env | Invoke-Expression`

4. Build docker images. **Note: you need to run these commands in the same terminal**

`docker-compose -f docker-compose.k8s-build.yml build`

8. Create namespace:

`kubectl create -f media-lake.namespace.yml`

8. Install istio and run:

`kubectl label namespace media-lake istio-injection=enabled`

10. Run k8s:

`kubectl apply -f kafka -f gateway-api -f users-api -f core-api -f gateway.yml -n media-lake`

11. Run `sudo minikube tunnel` and `kubectl get svc istio-ingressgateway -n istio-system` to get running service ip

## Useful commands:

1. Open minikube dashboard:

`minikube dashboard`

2. Restart deployment:

`kubectl rollout restart deployment/core-api-deployment -n=media-lake`

`kubectl rollout restart deploy -n=media-lake`

3. Get deployment logs:

`kubectl logs deploy/core-api-deployment -n=media-lake`

4. Delete deployments

`kubectl delete --all deployments -n=media-lake`

5. Delete everything:

`kubectl delete all --all -n media-lake`
