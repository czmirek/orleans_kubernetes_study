resource "kubernetes_role" "orleans_role" {
  metadata {
    name = "pod-reader"
  }
  rule {
    api_groups = [ "" ]
    resources = [ "pods" ]
    verbs = [ "get", "watch", "list" ]
  }
}

resource "kubernetes_role_binding" "orleans_role_binding" {
  metadata {
    name = "pod-reader-binding"
  }
  subject {
    kind = "ServiceAccount"
    name = "default"
    api_group = ""
  }
  role_ref {
    kind = "Role"
    name = "pod-reader"
    api_group = "rbac.authorization.k8s.io"
  }
}