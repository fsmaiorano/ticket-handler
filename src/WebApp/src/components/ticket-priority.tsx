import { TicketPriority as ImportedTicketPriority } from '@/models/ticket-priority';

interface TicketPriorityProps {
  priority: ImportedTicketPriority
}

const ticketPriorityMap: Record<ImportedTicketPriority, string> = {
  2: 'High',
  1: 'Medium',
  0: 'Low',
}

export function TicketPriority({ priority }: TicketPriorityProps) {
  return (
    <div className="flex items-center gap-2">
      {priority === 2 && (
        <span className="h-2 w-2 rounded-full bg-rose-500" />
      )}

      {priority === 0 && (
        <span className="h-2 w-2 rounded-full bg-emerald-500" />
      )}

      {priority === 1 && (
        <span className="h-2 w-2 rounded-full bg-amber-500" />
      )}

      <span className="font-medium text-muted-foreground">
        {ticketPriorityMap[priority]}
      </span>
    </div>
  )
}
