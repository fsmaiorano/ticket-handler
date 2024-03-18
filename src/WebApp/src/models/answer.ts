import { Ticket } from "./ticket"

export interface Answer {
  id: string
  content: string
  ticketId: string
  userId: string
  holderId: string
  sectorId: string
  ticket: Ticket
}
