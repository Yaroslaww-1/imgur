## How to run application using k8s
1. You need minikube installed locally.
2. Enable **Ingress** addon:

`minikube addons enable ingress`

3. Enable automatic images load to minikube. **Note: you need to run these commands in PowerShell**

`minikube docker-env | Invoke-Expression`

4. Build docker images. **Note: you need to run these commands in the same terminal**

`docker-compose -f docker-compose.k8s-build.yml build`

5. Create namespace:

`kubectl create -f media-lake.namespace.yml`

6. Install istio:

`istioctl install`

`kubectl label namespace media-lake istio-injection=enabled`

7. Run main app:

`kubectl apply -f kafka -f gateway-api -f users-api -f core-api -f vault -f elk -f gateway -n media-lake`

## How to run monitoring using k8s
1. Enable minikube metrics addon:

`minikube addons enable metrics-server`

2. Crate namespace:

`kubectl create -f monitoring.namespace.yml`

3. Install prometheus/grafana helm packages:

`helm repo add prometheus-community https://prometheus-community.github.io/helm-charts`

`helm install --namespace monitoring prometheus prometheus-community/kube-prometheus-stack`

`helm upgrade -f ./monitoring/additional-scrape-config.yml --namespace monitoring prometheus prometheus-community/kube-prometheus-stack`

4. Run monitoring:

`kubectl apply -f monitoring/k8s -n monitoring`

5. Forward required ports:

Grafana:

`kubectl port-forward --namespace monitoring service/prometheus-grafana 3000:80`

Prometheus:

` kubectl port-forward --namespace monitoring service/prometheus-operated 3001:9090`

6. Grafana credentials:

login: `admin`

password: try `prom-operator` or run `kubectl get secret --namespace media-lake prometheus-grafana -o jsonpath="{.data.admin-password}" | base64 --decode ; echo`

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
