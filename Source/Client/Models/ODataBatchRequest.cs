namespace BlazorApp1.Client.Models;

public record ODataBatchRequest(IEnumerable<ODataRequest> Requests);
