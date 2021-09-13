resource "kubernetes_service" "service" {
  metadata {
    name = "orleans-service"
  }
  spec {
    selector = {
      app = kubernetes_deployment.deploy.spec.0.template.0.metadata.0.labels.app
    }
    type = "NodePort"
    port {
      node_port   = 30201
      port        = 80
      target_port = 80
    }
  }
}