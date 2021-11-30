ui = true

storage "file" {
  path        = ".vault/data"
}

listener "tcp" {
  address     = "0.0.0.0:9100"
  tls_disable = 1
}

api_addr = "http://127.0.0.1:9100"
cluster_addr = "https://127.0.0.1:9101"