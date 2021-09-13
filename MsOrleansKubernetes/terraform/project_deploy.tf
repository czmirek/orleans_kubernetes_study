resource "kubernetes_deployment" "deploy" {
  metadata {
    name = "orleans"
  }
  spec {
    replicas = 4
    selector {
      match_labels = {
        app = "orleans"
      }
    }
    template {
      metadata {
        labels = {
          app                 = "orleans"
          "orleans/serviceId" = "myapp"
          "orleans/clusterId" = "mycluster"
        }
      }
      spec {
        container {
          image             = "orleans:latest"
          name              = "orleans"
          image_pull_policy = "Never"
          env {
            name="ORLEANS_SERVICE_ID"
            value_from {
              field_ref {
                field_path = "metadata.labels['orleans/serviceId']"
              }
            }
          }
          env {
            name="ORLEANS_CLUSTER_ID"
            value_from {
              field_ref {
                field_path = "metadata.labels['orleans/clusterId']"
              }
            }
          }
          env {
            name = "POD_NAME"
            value_from {
              field_ref {
                field_path = "metadata.name"
              }
            }
          }
          env {
            name = "POD_NAMESPACE"
            value_from {
              field_ref {
                field_path = "metadata.namespace"
              }
            }
          }
          env {
            name = "POD_IP"
            value_from {
              field_ref {
                field_path = "status.podIP"
              }
            }
          }
          port {
            name           = "web"
            container_port = 80
          }
          port {
            name           = "ssl"
            container_port = 443
          }
          port {
            name           = "orleans11111"
            container_port = 11111
          }
          port {
            name           = "orleans30000"
            container_port = 30000
          }
        }
      }
    }
  }
}