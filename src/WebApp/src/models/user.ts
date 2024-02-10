import { UserRoles } from './user-roles'

export interface User {
  id: string
  name: string
  email: string
  role: UserRoles
  holderId: string
  createdAt: string
  isActive: boolean
}
