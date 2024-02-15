import { TicketPriority } from "./ticket-priority"
import { TicketStatus } from "./ticket-status"

export interface Ticket {
  id: string
  title: string
  content: string
  status: TicketStatus
  priority: TicketPriority
  holderId: string
  sectorId: string
  assigneeId: string
  userId: string
  createdAt: string
  updatedAt: string
}
