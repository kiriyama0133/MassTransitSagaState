using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransitSagaState.Contracts.Events;

public record EmailSentRabbitMqEvent(
    Guid ProductId,
    string Name,
    string Description
);