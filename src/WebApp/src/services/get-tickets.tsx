import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface GetTicketsParams {
  holderId: string
  sector?: string
  title?: string
  status?: string
  priority?: string
  page?: number
  pageSize?: number
}

interface GetTicketsResponse {
  success: boolean
  message: string
  pageNumber: number
  totalPages: number
  items: Ticket[]
}

export async function getTickets({
  holderId,
  sector,
  title,
  status,
  priority,
  page,
  pageSize,
}: GetTicketsParams) {
  if (!page || page < 1) {
    page = 1
  }

  const url = `/api/ticket/holder/${holderId}?sector=${sector ?? ''}
  &title=${title ?? ''}
  &status=${status ?? ''}
  &priority=${priority ?? ''}
  &page=${page}
  &pageSize=${pageSize ?? 10}`

  const response = await api.get<GetTicketsResponse>(url.trim())

  return response.data
}
