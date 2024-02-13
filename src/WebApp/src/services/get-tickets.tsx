import { api } from '@/lib/axios'

export interface GetTicketsParams {
  holderId: string
}

export interface GetTicketResponse {
  id: string
  title: string
  content: string
  status: string
  priority: string
  holderId: string
  sectorId: string
  assigneeId: string
  userId: string
  createdAt: string
  updatedAt: string
}

export async function getTickets({ holderId }: GetTicketsParams) {
  const response = await api.get<GetTicketResponse[]>(
    `/api/ticket/holder/${holderId}`,
  )

  return response.data
}
