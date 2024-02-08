import { UserRoles } from './user-roles'

export interface User {
  name: string
  email: string
  role: UserRoles
  token: string
}
