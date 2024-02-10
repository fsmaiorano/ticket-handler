import { Sector } from './sector'

export interface Holder {
  id: string
  name: string
  sectors: Sector[]
  createdAt: string
  isActive: boolean
}
