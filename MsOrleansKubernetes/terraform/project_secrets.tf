resource "kubernetes_secret" "orleans_secrets" {
 for_each = local.orleans_secure_env
 metadata {
   name = replace(lower(each.key), "_", "-"
 }
 data = {
   "${each.key}" = base64encode(each.value)
 }
}