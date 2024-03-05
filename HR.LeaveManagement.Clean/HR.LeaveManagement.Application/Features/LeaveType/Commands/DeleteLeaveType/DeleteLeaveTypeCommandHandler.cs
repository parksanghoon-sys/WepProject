﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {

        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeToDelte = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if(leaveTypeToDelte == null )
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelte);

            return Unit.Value;
        }
    }
}
