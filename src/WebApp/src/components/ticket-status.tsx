export type TicketStatus = 'open' | 'active' | 'closed'

interface TicketStatusPros {
  status: TicketStatus
}

const ticketStatusMap: Record<TicketStatus, string> = {
  open: 'Open',
  active: 'Active',
  closed: 'Closed',
}

export function TicketStatus({ status }: TicketStatusPros) {
  return (
    <div className="flex items-center gap-2">
      {status === 'closed' && (
        <span className="h-2 w-2 rounded-full bg-rose-500" />
      )}

      {status === 'open' && (
        <span className="h-2 w-2 rounded-full bg-emerald-500" />
      )}

      {['active'].includes(status) && (
        <span className="h-2 w-2 rounded-full bg-amber-500" />
      )}

      <span className="font-medium text-muted-foreground">
        {ticketStatusMap[status]}
      </span>
    </div>
  )
}
