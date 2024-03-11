import { zodResolver } from '@hookform/resolvers/zod'
import { Search, X } from 'lucide-react'
import { Controller, useForm } from 'react-hook-form'
import { useSearchParams } from 'react-router-dom'
import { z } from 'zod'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'

import { TicketStatus } from '@/components/ticket-status'
import { AppContext } from '@/contexts/app-context'
import { TicketPriority } from '@/models/ticket-priority'
import { useContext } from 'react'

const orderFilterSchema = z.object({
  sector: z.string().optional(),
  title: z.string().optional(),
  status: z.string().optional(),
  priority: z.string().optional(),
})

type OrderFilterSchema = z.infer<typeof orderFilterSchema>

export function TicketTableFilter() {
  const { sectors } = useContext(AppContext)
  const [searchParams, setSearchParams] = useSearchParams()
  const sector = searchParams.get('sector')
  const title = searchParams.get('title')
  const status = searchParams.get('status')
  const priority = searchParams.get('priority')

  const { register, handleSubmit, control, reset } = useForm<OrderFilterSchema>(
    {
      resolver: zodResolver(orderFilterSchema),
      defaultValues: {
        sector: sector ?? 'all',
        title: title ?? '',
        status: status ?? 'all',
        priority: priority ?? 'all',
      },
    },
  )

  function handleFilter(data: OrderFilterSchema) {
    setSearchParams((state) => {
      if (data.sector) {
        state.set('sector', data.sector.trim())
      } else {
        state.delete('sector')
      }

      if (data.title) {
        state.set('title', data.title.trim())
      } else {
        state.delete('title')
      }

      if (data.status) {
        state.set('status', data.status.trim())
      } else {
        state.delete('status')
      }

      if (data.priority) {
        state.set('priority', data.priority.trim())
      } else {
        state.delete('priority')
      }

      state.set('page', '1')
      return state
    })
  }

  function handleClearFilters() {
    setSearchParams((state) => {
      state.delete('sector')
      state.delete('title')
      state.delete('status')
      state.delete('priority')
      state.set('page', '1')
      return state
    })

    reset({
      sector: 'all',
      title: '',
      status: 'all',
      priority: 'all',
    })
  }

  return (
    <>
      <form
        onSubmit={handleSubmit(handleFilter)}
        className="flex items-center gap-2"
      >
        <span className="text-sm font-semibold">Filters:</span>
        <Controller
          name="sector"
          control={control}
          render={({ field: { name, onChange, value, disabled } }) => {
            return (
              <Select
                defaultValue="all"
                name={name}
                onValueChange={onChange}
                value={value}
                disabled={disabled}
              >
                <SelectTrigger className="h-8 w-[180px]">
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="all">All</SelectItem>
                  {sectors.map((sector) => {
                    return (
                      <SelectItem key={sector.id} value={sector.name}>
                        {sector.name}
                      </SelectItem>
                    )
                  })}
                </SelectContent>
              </Select>
            )
          }}
        ></Controller>
        <Input
          className="h-8 w-[320px]"
          placeholder="Title"
          {...register('title')}
        />
        <Controller
          name="status"
          control={control}
          render={({ field: { name, onChange, value, disabled } }) => {
            return (
              <Select
                defaultValue="all"
                name={name}
                onValueChange={onChange}
                value={value}
                disabled={disabled}
              >
                <SelectTrigger className="h-8 w-[180px]">
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="all">All</SelectItem>
                  {Object.keys(TicketStatus).map((status) => {
                    return (
                      <SelectItem
                        key={status.toLocaleLowerCase()}
                        value={status}
                      >
                        {status}
                      </SelectItem>
                    )
                  })}
                </SelectContent>
              </Select>
            )
          }}
        ></Controller>
        <Controller
          name="priority"
          control={control}
          render={({ field: { name, onChange, value, disabled } }) => {
            return (
              <Select
                defaultValue="all"
                name={name}
                onValueChange={onChange}
                value={value}
                disabled={disabled}
              >
                <SelectTrigger className="h-8 w-[180px]">
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="all">All</SelectItem>
                  {Object.keys(TicketPriority).map((priority) => {
                    return (
                      <SelectItem
                        key={priority.toLocaleLowerCase()}
                        value={priority}
                      >
                        {priority}
                      </SelectItem>
                    )
                  })}
                </SelectContent>
              </Select>
            )
          }}
        ></Controller>
        <Button type="submit" variant="secondary" size="xs">
          <Search className="mr-2 h-4 w-4" />
          Filter results
        </Button>
        <Button
          onClick={handleClearFilters}
          type="button"
          variant="outline"
          size="xs"
        >
          <X className="mr-2 h-4 w-4" />
          Remove filters
        </Button>
      </form>
    </>
  )
}
